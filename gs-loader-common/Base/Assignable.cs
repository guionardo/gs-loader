using System;

namespace gs_loader_common.Base
{
    /// <summary>
    /// Classe base para assimilação das propriedades de outra instância
    /// </summary>
    public class Assignable : ICloneable
    {
        /// <summary>
        /// Copia as propriedades de outra instância da mesma classe
        /// </summary>
        /// <param name="another"></param>
        /// <returns></returns>
        public bool Assign(Assignable another)
        {
            if (another == null || this.GetType() != another.GetType())
                return false;

            var thispi = GetType().GetProperties();
            try
            {
                foreach (var pi in thispi)
                    if (pi.CanWrite)
                        pi.SetValue(this, pi.GetValue(another));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public object Clone()
        {
            Assignable r = (Assignable)Activator.CreateInstance(GetType());
            r.Assign(this);
            return r;
        }
    }
}
