using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledEnlightedItem : WorldItem, IEnlighted, IArticled
    {
        public Articler Articler { get; private set; }
        public Switch Switch { get; private set; }

        public ArticledEnlightedItem(
            string id, 
            uint space, 
            uint weight, 
            Genere genere, 
            Number number)
            : base(id, space, weight)
        {
            Articler = new Articler(genere, number);
            Switch = new Switch(false);
        }

        protected override object clone()
        {
            var clone = (ArticledEnlightedItem)instanciateClone();

            if (Switch.IsOn) clone.Switch.TurnOn();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ArticledEnlightedItem(Id, Space, Weight, Articler.Genere, Articler.Number);

        protected override void load(Save save)
        {
            Articler = save.GetSavable<Articler>(nameof(Articler));
            if (save.GetBool(nameof(Switch))) Switch.TurnOn();
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Articler), Articler)
                .With(nameof(Switch), Switch.IsOn);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
