using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledContainerOpenableFurniture : WorldItem, IContainer, IOpenable, IArticled, IFurniture
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public bool IsExternal { get; private set; }
        public Articler Articler { get; private set; }
        public Openable Openable { get; private set; }
        public LimitedInventory Inventory { get; private set; }

        public ArticledContainerOpenableFurniture(
            string id,
            uint space,
            uint weight,
            bool external,
            Genere genere,
            Number number,
            uint capacity,
            uint acceptedWeight)
            : base(id, space, weight)
        {
            this.capacity = capacity;
            this.acceptedWeight = acceptedWeight;
            IsExternal = external;
            Articler = new Articler(genere, number);
            Openable = new Openable(false);
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone()
        {
            var clone = (ArticledContainerOpenableFurniture)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ArticledContainerOpenableFurniture(Id, Space, Weight, IsExternal, Articler.Genere, Articler.Number, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            Articler = save.GetSavable<Articler>(nameof(Articler));
            Openable = save.GetSavable<Openable>(nameof(Openable));
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal)
                .WithSavable(nameof(Articler), Articler)
                .WithSavable(nameof(Inventory), Inventory)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
