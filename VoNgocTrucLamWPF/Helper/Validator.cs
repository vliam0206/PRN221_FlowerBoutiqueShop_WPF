using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoNgocTrucLamWPF.Helper;

public static class Validator
{
    public static bool IsNumeric(this string s) => int.TryParse(s, out _);
    public static bool IsDouble(this string s) => double.TryParse(s, out _);
}
