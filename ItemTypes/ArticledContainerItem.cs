using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledContainerItem : WorldItem, IContainer, IArticled
    {
        private readonly uint capacity;
        private readonly uint acceptedWeight;

        public Articler Articler { get; private set; }

        public LimitedInventory Inventory { get; private set; }

        public ArticledContainerItem(
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
            Inventory = new LimitedInventory(capacity, acceptedWeight);
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new ArticledContainerItem(Id, Space, Weight, Articler.Genere, Articler.Number, capacity, acceptedWeight);

        protected override void load(Save save)
        {
            Articler = save.GetSavable<Articler>(nameof(Articler));
            Inventory = save.GetSavable<LimitedInventory>(nameof(Inventory));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Articler), Articler)
                .WithSavable(nameof(Inventory), Inventory);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
