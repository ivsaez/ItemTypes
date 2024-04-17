using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class OpenableFurniture : WorldItem, IOpenable, IFurniture
    {
        public bool IsExternal { get; private set; }
        public Openable Openable { get; private set; }

        public OpenableFurniture(string id, uint space, uint weight, bool external)
            : base(id, space, weight)
        {
            IsExternal = external;
            Openable = new Openable(false);
        }

        protected override object clone()
        {
            var clone = (OpenableFurniture)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new OpenableFurniture(Id, Space, Weight, IsExternal);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            Openable = save.GetSavable<Openable>(nameof(Openable));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
