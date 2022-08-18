using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GGL.IO;

namespace DOF1Converter.Converter;

public class Model
{
    public List<Material> Materials = new();
    public List<Mesh> Meshes = new();

    public bool HasSimilarMaterial(Material material)
    {
        foreach (var mat in Materials)
        {
            if (mat.Name == material.Name)
                return true;
        }
        return false;
    }

    public static Model Merge(IList<Model> models)
    {
        var groupmodel = new Model();
        foreach (var model in models)
        {
            foreach (var mesh in model.Meshes)
            {
                groupmodel.Meshes.Add(mesh);
            }

            foreach (var mat in model.Materials)
            {
                if (!groupmodel.HasSimilarMaterial(mat))
                    groupmodel.Materials.Add(mat);
            }
        }


        return groupmodel;
    }
}

