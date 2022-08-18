using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DOF1Converter.Converter;

public class Mesh
{
    public int MaterialId;
    public Material Material;

    public int[] Indices;
    public Vertex[] Vertices;
    public record struct Vertex(Vector3 Position, Vector2 Uv, Vector3 Normal);
}
