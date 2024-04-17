using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ContainerOpenableItem: WorldItem, IContainer, IOpenable
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public Openable Openable { get; private set; }
        public LimitedInventory Inventory { get; private set; }

        public ContainerOpenableItem(
            string id, 
            uint space, 
            uint weight,
            uint capacity,
            uint acceptedWeight)
            : base(id, space, weight)
        {
            this.capacity = capacity;
            this.acceptedWeight = acceptedWeight;
            Openable = new Openable(false);
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone()
        {
            var clone = (ContainerOpenableItem)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ContainerOpenableItem(Id, Space, Weight, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            Openable = save.GetSavable<Openable>(nameof(Openable));
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Inventory), Inventory)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
