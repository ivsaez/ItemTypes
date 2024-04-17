using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ContainerOpenableFurniture : WorldItem, IContainer, IOpenable, IFurniture
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public bool IsExternal { get; private set; }
        public Openable Openable { get; private set; }
        public LimitedInventory Inventory { get; private set; }

        public ContainerOpenableFurniture(
            string id,
            uint space,
            uint weight,
            bool external,
            uint capacity,
            uint acceptedWeight)
            : base(id, space, weight)
        {
            this.capacity = capacity;
            this.acceptedWeight = acceptedWeight;
            IsExternal = external;
            Openable = new Openable(false);
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone()
        {
            var clone = (ContainerOpenableFurniture)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ContainerOpenableFurniture(Id, Space, Weight, IsExternal, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            Openable = save.GetSavable<Openable>(nameof(Openable));
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal)
                .WithSavable(nameof(Inventory), Inventory)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
