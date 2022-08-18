using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GGL.IO;
using System.Reflection.PortableExecutable;
using System.Drawing;
using System.Windows.Forms;

namespace DOF1Converter.Converter;

public class DOFFileReader : BinaryViewReader
{

    public DOFFileReader(string path) : base(path)
    {
        DefaultCharSize = CharSize.Byte;
    }
    public DOFFileReader(Stream stream) : base(stream)
    {
        DefaultCharSize = CharSize.Byte;
    }
    public static Model Load(string path)
    {
        var model = new Model();
        using var br = new DOFFileReader(path);
        br.ReadAllContentToModel(model);
        return model;
    }

    void ReadAllContentToModel(Model target)
    {
        AssertMagic(String4.DOF1);
        uint fileSize = ReadUInt32();
        if (PeakStream.Length != fileSize + 8)
            throw new InvalidDataException("Invalid file size.");


        var handlers = new Handler[]
        {
            new(String4.MATS, () => ReadDOFArrayToList(target.Materials, ReadToMaterial)),
            new(String4.GEOB, () => ReadDOFArrayToList(target.Meshes, ReadToMesh)),
        };
        StartReadLoop(String4.EDOF, handlers);

        foreach (var mesh in target.Meshes)
        {
            mesh.Material = target.Materials[mesh.MaterialId];
        }
    }

    void ReadToMesh(Mesh target)
    {
        AssertMagic(String4.GOB1);
        BeginDOFSection();

        ushort[] indi = null;
        Vector3[] vert = null;
        Vector2[] tver = null;
        Vector3[] norm = null;

        var handlers = new Handler[]
        {
            new(String4.GHDR, () =>
            {
                Seek(8, SeekOrigin.Current);
                target.MaterialId = ReadInt32();
            }),
            new(String4.INDI, () => indi = ReadArray<ushort>(LengthPrefix.UInt32)),
            new(String4.VERT, () => vert = ReadArray<Vector3>(LengthPrefix.UInt32)),
            new(String4.TVER, () => tver = ReadArray<Vector2>(LengthPrefix.UInt32)),
            new(String4.NORM, () => norm = ReadArray<Vector3>(LengthPrefix.UInt32)),
        };
        StartReadLoop(String4.GEND, handlers);

        EndDOFSection();

        var indices = target.Indices = new int[indi.Length];
        var vertices = target.Vertices = new Mesh.Vertex[vert.Length];

        for (int i = 0; i < indices.Length; i++)
            indices[i] = indi[i];

        for (int i = 0; i < vertices.Length; i++)
            vertices[i] = new Mesh.Vertex(vert[i], tver[i], norm[i]);
    }

    void ReadToMaterial(Material target)
    {
        AssertMagic(String4.MAT0);
        BeginDOFSection();

        var handlers = new Handler[]
        {
            new(String4.MHDR, () =>
            {
                target.Name = ReadString(LengthPrefix.UInt16);
                string className = ReadString(LengthPrefix.UInt16);
            }),
            new(String4.MCOL, () =>
            {
                target.Ambient = Read<FColor>();
                target.Diffuse = Read<FColor>();
                target.Specular = Read<FColor>();
                target.Emission = Read<FColor>();
                target.Shininess = ReadSingle();
            }),
            new(String4.MTEX, () =>
            {
                uint texCount = ReadUInt32();
                if (texCount == 0)
                    return;
                else if (texCount == 1)
                    target.Texture = ReadString(LengthPrefix.UInt16);
                else
                    throw new IndexOutOfRangeException($"TexCount: {texCount}.");
            }),
        };
        StartReadLoop(String4.MEND, handlers);

        EndDOFSection();
    }


    void StartReadLoop(String4 exit, Handler[] handlers)
    {
        int i = 0;
        while (true)
        {
            var type = Read<String4>();

            if (type == exit)
                return;

            bool found = false;
            foreach (var handler in handlers)
            {
                if (type == handler.Match)
                {
                    BeginDOFSection();
                    handler.Action();
                    EndDOFSection(RestAction.Ignore);

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                BeginDOFSection();
                EndDOFSection(RestAction.Jump);
            }

            i++;
            if (i > 1000)
                throw new InvalidDataException();
        }
    }

    // Expected section
    void AssertMagic(String4 expected)
    {
        String4 magic = Read<String4>();
        if (magic != expected)
            throw new InvalidDataException($"Unexpected Magic at {Position}, Expected: '{expected}' != Value: '{magic}'.");
    }

    void ReadDOFArrayToList<T>(List<T> list, Action<T> reader) where T : new()
    {
        uint count = ReadUInt32();
        for (int i = 0; i < count; i++)
        {
            var obj = new T();
            reader(obj);
            list.Add(obj);
        }
    }

    void BeginDOFSection()
    {
        uint size = ReadUInt32();
        uint endPos = (uint)Position + size;
        var args = new DOFSectionArgs(size, endPos);
        StreamStack.Push(PeakStream, false, args);
    }

    void EndDOFSection(RestAction restAction = RestAction.Exception)
    {
        var sec = StreamStack.Pop();
        if (sec.Args is DOFSectionArgs)
        {
            var args = (DOFSectionArgs)sec.Args;
            if (Position != args.FinalPosition)
            {
                if (restAction == RestAction.Jump)
                {
                    Seek(args.FinalPosition, SeekOrigin.Begin);
                }
                else if (restAction == RestAction.Exception)
                {
                    throw new InvalidDataException($"Incomplete section Expected: {args.FinalPosition} != Position {Position},");
                }
            }
        }
        else
        {
            throw new InvalidOperationException("Args type !DOFSectionArgs,");
        }
    }

    class Handler
    {
        public String4 Match;
        public Action Action;
        public int Offset;

        public Handler(String4 match, Action action)
        {
            Match = match;
            Action = action;
            Offset = 0;
        }

        public Handler(String4 match, int offset, Action action)
        {
            Match = match;
            Action = action;
            Offset = offset;
        }
    }

    class DOFSectionArgs
    {
        public uint Size;
        public uint FinalPosition;

        public DOFSectionArgs(uint size, uint finalPosition)
        {
            Size = size;
            FinalPosition = finalPosition;
        }
    }

    enum RestAction
    {
        Ignore,
        Jump,
        Exception
    }
}
