using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DOF1Converter.Converter;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public unsafe struct String4
{
    public static implicit operator String4(uint input) => *(String4*)&input;

    public static implicit operator string(String4 input)
    {
        byte* ptr = (byte*)&input;
        char[] chars = new char[4];
        chars[0] = (char)ptr[0];
        chars[1] = (char)ptr[1];
        chars[2] = (char)ptr[2];
        chars[3] = (char)ptr[3];
        return new string(chars);
    }

    public static explicit operator String4(string input)
    {
        var obj = new String4();
        var ptr = (byte*)&obj;
        ptr[0] = (byte)input[0];
        ptr[1] = (byte)input[1];
        ptr[2] = (byte)input[2];
        ptr[3] = (byte)input[3];
        return obj;
    }

    public override string ToString() => (string)this;

    public readonly static String4 DOF1 = (String4)"DOF1";
    public readonly static String4 EDOF = (String4)"EDOF";

    public readonly static String4 MATS = (String4)"MATS";
    public readonly static String4 GEOB = (String4)"GEOB";
    public readonly static String4 MAT0 = (String4)"MAT0";
    public readonly static String4 GOB1 = (String4)"GOB1";

    public readonly static String4 MHDR = (String4)"MHDR";
    public readonly static String4 MCOL = (String4)"MCOL";
    public readonly static String4 MCFL = (String4)"MCFL";
    public readonly static String4 MUVW = (String4)"MUVW";
    public readonly static String4 MTRA = (String4)"MTRA";
    public readonly static String4 MTEX = (String4)"MTEX";
    public readonly static String4 MSUB = (String4)"MSUB";
    public readonly static String4 MEND = (String4)"MEND";

    public readonly static String4 GHDR = (String4)"GHDR";
    public readonly static String4 INDI = (String4)"INDI";
    public readonly static String4 VERT = (String4)"VERT";
    public readonly static String4 TVER = (String4)"TVER";
    public readonly static String4 NORM = (String4)"NORM";
    public readonly static String4 GEND = (String4)"GEND";
}
