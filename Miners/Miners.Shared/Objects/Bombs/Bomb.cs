using Miners.Shared.Objects.Base;
using Newtonsoft.Json;
using OpenTK;

namespace Miners.Shared.Objects.Bombs
{
    public class Bomb : GameObject, IBomb
    {
        private const int _defaultRadius = 1;
        private const int _defaultDamage = 1;

        /// <summary>The time before expode</summary>
        public const int TimeBeforeExpode = 2;

        /// <inheritdoc />
        public int Radius { get; set; }

        /// <inheritdoc />
        public int Damage { get; set; }

        /// <inheritdoc />
        public override string Type => nameof(Bomb);

        /// <summary>Initializes a new instance of the <see cref="Bomb" /> class.</summary>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public Bomb(Vector2 position, string path) : base(position, path)
        {
            Radius = _defaultRadius;
            Damage = _defaultDamage;
        }

        /// <summary>Initializes a new instance of the <see cref="Bomb" /> class.</summary>
        /// <param name="radius">The radius.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="position">The position.</param>
        /// <param name="path">The path.</param>
        public Bomb(int radius, int damage, Vector2 position, string path) : base(position, path)
        {
            Radius = radius;
            Damage = damage;
        }

        /// <summary>Initializes a new instance of the <see cref="Bomb" /> class. Json needed and tests needed.</summary>
        public Bomb() : base(default, null) { }

        //public Bomb DeepCopy()
        //{
        //    var settings = new JsonSerializerSettings
        //    {
        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //    };
        //    var json = JsonConvert.SerializeObject(this, settings);
        //    return JsonConvert.DeserializeObject<Bomb>(json);
        //}
    }
}