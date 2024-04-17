using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ContainerItem: WorldItem, IContainer
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public LimitedInventory Inventory { get; private set; }

        public ContainerItem(
            string id, 
            uint space, 
            uint weight, 
            uint capacity, 
            uint acceptedWeight)
            : base(id, space, weight)
        {
            this.capacity = capacity;
            this.acceptedWeight = acceptedWeight;
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new ContainerItem(Id, Space, Weight, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Inventory), Inventory);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
