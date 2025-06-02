using Rampastring.Tools;

namespace MapCleaner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parallel.ForEach(args, arg =>
            {
                bool findoutSection = false;
                IniFile mapSample = new IniFile(arg);

                List<string> list = new List<string> { "YAYARD", "NAYARD", "GAYARD", "ZAYARD", "TAYARD", "XAYARD" };

                foreach (var sectionName in list)
                {
                    if (
                        mapSample.SectionExists(sectionName) &&
                        mapSample.KeyExists(sectionName, "TechLevel") &&
                        ((mapSample.GetIntValue(sectionName, "TechLevel", 0) > 10) || (mapSample.GetIntValue(sectionName, "TechLevel", 0) == -1))
                       )
                    {
                        mapSample.RemoveSection(sectionName);
                        findoutSection = true;
                    }
                }

                if (findoutSection)
                {
                    if (!mapSample.SectionExists("#include"))
                    {
                        mapSample.AddSection("#include");
                        mapSample.SetStringValue("#include", "1", "yard_disable.ini");
                        mapSample.MoveSectionToFirst("#include");

                    }
                }

                mapSample.WriteIniFile();
            });
        }
    }
}