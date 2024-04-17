using Saver;
using Worlding;

namespace ItemTypes
{
    public class SimpleItem : WorldItem
    {
        public SimpleItem(string id, uint space, uint weight)
            : base(id, space, weight)
        {
        }

        protected override object clone() =>
            instanciateClone();

        protected virtual object instanciateClone() =>
            new SimpleItem(Id, Space, Weight);

        protected override void load(Save save)
        {
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id);
            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
