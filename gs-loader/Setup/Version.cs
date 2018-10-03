using System;

namespace gs_loader.Setup
{
    public class Version : IComparable
    {
        public Version(string version)
        {
            var v = (version ?? "").Split('.');
            Major = int.TryParse(v.Length > 0 ? v[0] : "0", out int n) ? n : 0;
            Minor = int.TryParse(v.Length > 1 ? v[1] : "0", out n) ? n : 0;
            Build = int.TryParse(v.Length > 2 ? v[2] : "0", out n) ? n : 0;
            Revision = int.TryParse(v.Length > 3 ? v[3] : "0", out n) ? n : 0;
        }
        public Version()
        {
            Major = Minor = Build = Revision = 0;
        }

        public int Build { get; set; } = 0;
        public int Major { get; set; } = 0;
        public int Minor { get; set; } = 0;
        public int Revision { get; set; } = 0;
        public int CompareTo(object obj)
        {
            int c = 0;
            Version o;
            if (obj is Version)
                o = (Version)obj;
            else if (obj is string)
                o = new Version(obj.ToString());
            else if (obj is System.Version)
                o = new Version(((System.Version)obj).ToString());
            else
                return 0;

            c = Major.CompareTo(o.Major) * 1000 +
                Minor.CompareTo(o.Minor) * 100 +
                Build.CompareTo(o.Build) * 10 +
                Revision.CompareTo(o.Revision);
            c = (c < 0 ? -1 : c > 0 ? 1 : 0);

            return c;
        }

        public override bool Equals(object obj) => CompareTo(obj) == 0;
        public override int GetHashCode() => ToString().GetHashCode();

        public override string ToString() => string.Format("{0}.{1}.{2}.{3}", Major, Minor, Build, Revision);
    }
}
