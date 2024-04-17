using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledContainerOpenableItem : WorldItem, IContainer, IOpenable, IArticled
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public Articler Articler { get; private set; }
        public Openable Openable { get; private set; }
        public LimitedInventory Inventory { get; private set; }

        public ArticledContainerOpenableItem(
            string id,
            uint space,
            uint weight,
            Genere genere,
            Number number,
            uint capacity,
            uint acceptedWeight)
            : base(id, space, weight)
        {
            this.capacity = capacity;
            this.acceptedWeight = acceptedWeight;
            Articler = new Articler(genere, number);
            Openable = new Openable(false);
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone()
        {
            var clone = (ArticledContainerOpenableItem)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ArticledContainerOpenableItem(Id, Space, Weight, Articler.Genere, Articler.Number, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            Articler = save.GetSavable<Articler>(nameof(Articler));
            Openable = save.GetSavable<Openable>(nameof(Openable));
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Articler), Articler)
                .WithSavable(nameof(Inventory), Inventory)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
