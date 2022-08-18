using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DOF1Converter.Converter;

public class ObjFileWriter : IDisposable
{
    int idxOffset = 1;
    int meshCount = 0;

    Stream objStream;
    StreamWriter objWriter;

    Stream mtlStream;
    StreamWriter mtlWriter;

    private bool disposedValue;

    public ObjFileWriter(string path)
    {
        objStream = new FileStream(path, FileMode.Create);
        objWriter = new StreamWriter(objStream);

        string mtlPath = Path.Join(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + ".mtl");
        mtlStream = new FileStream(mtlPath, FileMode.Create);
        mtlWriter = new StreamWriter(mtlStream);
    }

    public static void Save(Model target, string path)
    {
        var writer = new ObjFileWriter(path);

        if (target.Meshes.Count < 1)
            return;

        writer.WriteModel(target);

        writer.Dispose();
    }

    public void WriteModel(Model target)
    {
        var meshes = target.Meshes;
        var materials = target.Materials;

        objWriter.WriteLine($"#Model mesh:{meshes.Count} mat:{materials.Count}");
        for (int i = 0; i < meshes.Count; i++)
        {
            var mesh = meshes[i];
            var vertices = mesh.Vertices;
            var indices = mesh.Indices;

            string meshName = $"mesh_{meshCount++}";
            objWriter.WriteLine();
            objWriter.WriteLine($"#Mesh vtx:{vertices.Length} idx:{indices.Length}");
            objWriter.WriteLine($"o {meshName}");
            objWriter.WriteLine($"usemtl {mesh.Material.Name}");

            foreach (var vtx in mesh.Vertices)
                objWriter.WriteLine($"v {vtx.Position.X} {vtx.Position.Y} {vtx.Position.Z}");

            foreach (var vtx in mesh.Vertices)
                objWriter.WriteLine($"vt {vtx.Uv.X} {vtx.Uv.Y}");

            foreach (var vtx in mesh.Vertices)
                objWriter.WriteLine($"vn {vtx.Normal.X} {vtx.Normal.Y} {vtx.Normal.Z}");

            for (int iIdx = 0; iIdx < indices.Length; iIdx += 3)
            {
                int idx0 = indices[iIdx + 0] + idxOffset;
                int idx1 = indices[iIdx + 1] + idxOffset;
                int idx2 = indices[iIdx + 2] + idxOffset;
                objWriter.WriteLine($"f {idx0}/{idx0}/{idx0} {idx1}/{idx1}/{idx1} {idx2}/{idx2}/{idx2}");
            }

            idxOffset += mesh.Vertices.Length;
        }

        for (int i = 0; i < materials.Count; i++)
        {
            var material = materials[i];

            mtlWriter.WriteLine();
            mtlWriter.WriteLine($"newmtl {material.Name}");

            mtlWriter.WriteLine($"  ka {material.Ambient.R} {material.Ambient.G} {material.Ambient.B}");
            mtlWriter.WriteLine($"  kd {material.Diffuse.R} {material.Diffuse.G} {material.Diffuse.B}");
            mtlWriter.WriteLine($"  ks {material.Specular.R} {material.Specular.G} {material.Specular.B}");

            if (material.Texture != null)
                mtlWriter.WriteLine($"  map_Kd {material.Texture}");
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            objStream.Dispose();
            mtlStream.Dispose();

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~ObjFileWriter()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
