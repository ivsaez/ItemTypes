using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ContainerFurniture : WorldItem, IContainer, IFurniture
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public bool IsExternal { get; private set; }
        public LimitedInventory Inventory { get; private set; }

        public ContainerFurniture(
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
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new ContainerFurniture(Id, Space, Weight, IsExternal, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal)
                .WithSavable(nameof(Inventory), Inventory);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
