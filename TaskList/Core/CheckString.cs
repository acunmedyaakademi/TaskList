namespace TaskList.Core
{
    public class CheckString
    {
        public static bool Check(string value)
        {
            bool result = true;
            if (value.Contains("script")) result = false;
            if (value.Contains("SCRIPT")) result = false;
            if (value.Contains("alert")) result = false;
            if (value.Contains("$")) result = false;
            if (value.Contains("~")) result = false;
            if (value.Contains("[")) result = false;
            if (value.Contains("^")) result = false;
            if (value.Contains("<")) result = false;
            if (value.Contains(">")) result = false;
            if (value.Contains("--")) result = false;
            if (value.Contains("`")) result = false;
            if (value.Contains("\\")) result = false;
            if (value.Contains("#")) result = false;
            if (value.Contains("/*")) result = false;
            if (value.Contains("*/")) result = false;
            if (value.Contains("SELECT")) result = false;
            return result;
        }
    }
}
