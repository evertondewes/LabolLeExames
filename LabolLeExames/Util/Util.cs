using System;
using System.IO;
using System.Text;

/**
* Utility methods.
*/
public class Util
{

    /**
    * Converts a byte to a hexadecimal string value.
    */
    public static string ToHexString(int b)
    {
        const int mask1 = 0x0F;
        const int mask2 = 0xF0;

        string ret = "";

        int c1 = (b & mask1);
        int c2 = (b & mask2) >> 4;

        ret = ret + ToHexChar(c2) + ToHexChar(c1);
        return ret;
    }

    /**
    * Converts the given byte to a hex character.
    */
    private static char ToHexChar(int b)
    {
        const int ascii_zero = 48;
        const int ascii_a = 65;

        if (b >= 0 && b <= 9)
        {
            return (char)(b + ascii_zero);
        }
        if (b >= 10 && b <= 15)
        {
            return (char)(b + ascii_a - 10);
        }
        return '?';
    }

    /**
    * Converts the byte to a visible ASCII character or
    * underscore if it is not.
    */
    public static char ToSafeAscii(int b)
    {
        if (b >= 32 && b <= 126)
        {
            return (char)b;
        }
        else
        {
            if (b == 0)
            {
                return '_';
            }
            if (b == 128)
            {
                return 'Ç';
            }
            if (b == 130)
            {
                return 'é';
            }
            if (b == 135)
            {
                return 'ç';
            }
            if (b == 136)
            {
                return 'ê';
            }
            if (b == 144)
            {
                return 'É';
            }
            if (b == 147)
            {
                return 'ô';
            }
            if (b == 154)
            {
                return 'Ü';
            }
            if (b == 160)
            {
                return 'á';
            }
            if (b == 161)
            {
                return 'í';
            }
            if (b == 162)
            {
                return 'ó';
            }
            if (b == 163)
            {
                return 'ú';
            }
            if (b == 166)
            {
                return 'ª';
            }
            if (b == 185)
            {
                return 'Á';
            }
            if (b == 198)
            {
                return 'ã';
            }
            if (b == 229)
            {
                return 'Ã';
            }
            if (b == 252)
            {
                return '³';
            }
         }
        return '_';
    }

    public static string getTexto(ref FileStream fs, int tamanho)
    {
        StringBuilder sbDado = new StringBuilder();
        for (int i = 0; (i < tamanho) && fs.CanRead; i++)
        {
            int b = fs.ReadByte();
            if (b == -1)
            {
                //fs.Close();
                break;
            }
            sbDado.Append(Util.ToSafeAscii(b));
        }
        return sbDado.ToString();
    }

    public static string getHexa(ref FileStream fs, int tamanho)
    {
        StringBuilder sbDado = new StringBuilder();
        for (int i = 0; (i < tamanho) && fs.CanRead; i++)
        {
            int b = fs.ReadByte();
            if (b == -1)
            {
                //fs.Close();
                break;
            }
            sbDado.Append(Util.ToHexString(b));
        }
        return sbDado.ToString();
    }
}

