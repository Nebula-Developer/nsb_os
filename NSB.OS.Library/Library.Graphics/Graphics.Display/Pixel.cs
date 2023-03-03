using NSB.OS.Graphics.Mathematics;
using NSB.OS.Graphics;
using System.Collections.Generic;

namespace NSB.OS.Graphics.DisplayNS;

public class Pixel {
    public RGB? BG { get; set; }
    public RGB? FG { get; set; }
    public char? Character { get; set; }

    public Pixel(char? character = null, RGB? bg = null, RGB? fg = null) {
        Character = character;
        BG = bg;
        FG = fg;
    }

    public string ToBGEsc() => BG != null ? $"\x1b[48;2;{BG.R};{BG.G};{BG.B}m" : "";
    public string ToFGEsc() => FG != null ? $"\x1b[38;2;{FG.R};{FG.G};{FG.B}m" : "";

    public override string ToString() {
        return $"{ToBGEsc()}{ToFGEsc()}{Character ?? ' '}\x1b[0m";   
    }

    // == and !=
    public static bool operator ==(Pixel a, Pixel b) => a?.Character == b?.Character && a?.BG == b?.BG && a?.FG == b?.FG;
    public static bool operator !=(Pixel a, Pixel b) => a?.Character != b?.Character || a?.BG != b?.BG || a?.FG != b?.FG;
    public override bool Equals(object? obj) => obj is Pixel pixel && this == pixel;
    public override int GetHashCode() => HashCode.Combine(BG, FG, Character);
}