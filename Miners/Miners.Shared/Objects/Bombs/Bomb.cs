using Miners.Shared.Objects.Base;
using Newtonsoft.Json;
using OpenTK;

namespace Miners.Shared.Objects.Bombs
{
    public class Bomb : GameObject, IBomb
    {
        private const int _defaultRadius = 1;
        private const int _defaultDamage = 1;

        public const int TimeBeforeExpode = 2;

        public int Radius { get; set; }
        public int Damage { get; set; }
        public override string Type => nameof(Bomb);

        public Bomb(Vector2 position, string path) : base(position, path)
        {
            Radius = _defaultRadius;
            Damage = _defaultDamage;
        }

        public Bomb(int radius, int damage, Vector2 position, string path) : base(position, path)
        {
            Radius = radius;
            Damage = damage;
        }

        // Json needed and tests needed
        public Bomb() : base(default, null) { }

        public Bomb DeepCopy()
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(this, settings);
            return JsonConvert.DeserializeObject<Bomb>(json);
        }
    }
}