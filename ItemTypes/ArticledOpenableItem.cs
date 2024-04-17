using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledOpenableItem : WorldItem, IArticled, IOpenable
    {
        public Articler Articler { get; private set; }
        public Openable Openable { get; private set; }

        public ArticledOpenableItem(
            string id, 
            uint space, 
            uint weight, 
            Genere genere, 
            Number number)
            : base(id, space, weight)
        {
            Articler = new Articler(genere, number);
            Openable = new Openable(false);
        }

        protected override object clone()
        {
            var clone = (ArticledOpenableItem)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new ArticledOpenableItem(Id, Space, Weight, Articler.Genere, Articler.Number);

        protected override void load(Save save)
        {
            Articler = save.GetSavable<Articler>(nameof(Articler));
            Openable = save.GetSavable<Openable>(nameof(Openable));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Articler), Articler)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
