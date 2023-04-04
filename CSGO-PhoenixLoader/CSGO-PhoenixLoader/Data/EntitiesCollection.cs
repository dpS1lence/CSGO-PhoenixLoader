using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.Data
{
    public static class EntitiesCollection
    {
        public static ICollection<Entity> Entities { get; set; } = new HashSet<Entity>();

        public static ICollection<Entity> IgnoredEntities { get; private set; } = new HashSet<Entity>();

        public static ICollection<Entity> EntitiesDynamic
        {
            get
            {
                var entities = new HashSet<Entity>(Entities);

                foreach (var entity in entities.Where(entity => IgnoredEntities.Contains(entity)))
                {
                    entities.Remove(entity);
                }

                return entities;
            }
        }

        public static void ResetIgnoredEntities()
        {
            IgnoredEntities = new HashSet<Entity>();
        }
    }
}
