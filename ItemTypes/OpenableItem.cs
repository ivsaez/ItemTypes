using Items;
using Saver;
using Worlding;

namespace ItemTypes
{
    public class OpenableItem : WorldItem, IOpenable
    {
        public Openable Openable { get; private set; }

        public OpenableItem(string id, uint space, uint weight, IItem? key = null)
            : base(id, space, weight)
        {
            Openable = new Openable(false, key);
        }

        protected override object clone() 
        { 
            var clone = (OpenableItem)instanciateClone();

            clone.Openable = (Openable)Openable.Clone();

            return clone;
        }

        protected virtual object instanciateClone() =>
            new OpenableItem(Id, Space, Weight);

        protected override void load(Save save)
        {
            Openable = save.GetSavable<Openable>(nameof(Openable));
            loadParticular(save);
        }

        protected virtual void loadParticular(Save save) { }

        protected override Save save()
        {
            var save = new Save(Id)
                .WithSavable(nameof(Openable), Openable);

            saveParticular(save);

            return save;
        }

        protected virtual void saveParticular(Save save) { }
    }
}
