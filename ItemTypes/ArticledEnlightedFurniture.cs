using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledEnlightedFurniture : WorldItem, IFurniture, IEnlighted, IArticled
    {
        public Articler Articler { get; private set; }
        public Switch Switch { get; private set; }
        public bool IsExternal { get; private set; }

        public ArticledEnlightedFurniture(
            string id, 
            uint space, 
            uint weight, 
            Genere genere,
            Number number)
            : base(id, space, weight)
        {
            IsExternal = false;
            Articler = new Articler(genere, number);
            Switch = new Switch(false);
        }

        protected override object clone()
        {
            var clone = (ArticledEnlightedFurniture)instanciateClone();

            if (Switch.IsOn) clone.Switch.TurnOn();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ArticledEnlightedFurniture(Id, Space, Weight, Articler.Genere, Articler.Number);

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
