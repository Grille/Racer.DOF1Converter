using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DOF1Converter.Converter;

public class Material
{
    public string Name;
    public string Texture;

    public FColor Ambient;
    public FColor Diffuse;
    public FColor Specular;
    public FColor Emission;
    public float Shininess;

    public Vector2 UvOffset;
}
