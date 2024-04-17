using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class ArticledFurniture : WorldItem, IFurniture, IArticled
    {
        public bool IsExternal { get; private set; }
        public Articler Articler { get; private set; }

        public ArticledFurniture(
            string id, 
            uint space, 
            uint weight, 
            bool external, 
            Genere genere,
            Number number)
            : base(id, space, weight)
        {
            IsExternal = external;
            Articler = new Articler(genere, number);
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new ArticledFurniture(Id, Space, Weight, IsExternal, Articler.Genere, Articler.Number);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            Articler = save.GetSavable<Articler>(nameof(Articler));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal)
                .WithSavable(nameof(Articler), Articler);
            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
