using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledOpenableFurniture : WorldItem, IArticled, IOpenable, IFurniture
    {
        public bool IsExternal { get; private set; }
        public Articler Articler { get; private set; }
        public Openable Openable { get; private set; }

        public ArticledOpenableFurniture(
            string id,
            uint space,
            uint weight,
            bool external,
            Genere genere,
            Number number)
            : base(id, space, weight)
        {
            IsExternal = external;
            Articler = new Articler(genere, number);
            Openable = new Openable(false);
        }

        protected override object clone()
        {
            var clone = (ArticledOpenableFurniture)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ArticledOpenableFurniture(Id, Space, Weight, IsExternal, Articler.Genere, Articler.Number);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            Articler = save.GetSavable<Articler>(nameof(Articler));
            Openable = save.GetSavable<Openable>(nameof(Openable));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal)
                .WithSavable(nameof(Articler), Articler)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
