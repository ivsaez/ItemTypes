using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledContainerFurniture : WorldItem, IContainer, IArticled, IFurniture
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public bool IsExternal { get; private set; }
        public Articler Articler { get; private set; }
        public LimitedInventory Inventory { get; private set; }

        public ArticledContainerFurniture(
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
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new ArticledContainerFurniture(Id, Space, Weight, IsExternal, Articler.Genere, Articler.Number, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            Articler = save.GetSavable<Articler>(nameof(Articler));
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal)
                .WithSavable(nameof(Articler), Articler)
                .WithSavable(nameof(Inventory), Inventory);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
