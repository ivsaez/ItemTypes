using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class SimpleFurniture : WorldItem, IFurniture
    {
        public bool IsExternal {  get; private set; }

        public SimpleFurniture(string id, uint space, uint weight, bool external)
            : base(id, space, weight)
        {
            IsExternal = external;
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new SimpleFurniture(Id, Space, Weight, IsExternal);

        protected override void load(Save save)
        {
            IsExternal = save.GetBool(nameof(IsExternal));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .With(nameof(IsExternal), IsExternal);
            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
