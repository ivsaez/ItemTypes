using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledItem : WorldItem, IArticled
    {
        public Articler Articler { get; private set; }

        public ArticledItem(
            string id, 
            uint space, 
            uint weight, 
            Genere genere, 
            Number number)
            : base(id, space, weight)
        {
            Articler = new Articler(genere, number);
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new ArticledItem(Id, Space, Weight, Articler.Genere, Articler.Number);

        protected override void load(Save save)
        {
            Articler = save.GetSavable<Articler>(nameof(Articler));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Articler), Articler);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
