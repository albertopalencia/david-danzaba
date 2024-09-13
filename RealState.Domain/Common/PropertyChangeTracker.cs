namespace RealState.Domain.Common
{
    public static class PropertyChangeTracker
    {
        public static T CreateEntityWithChangedProperties<T>(T original, T modified) where T : new()
        { 
            var changedEntity = new T();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var originalValue = property.GetValue(original);
                var modifiedValue = property.GetValue(modified);

                if (!Equals(originalValue, modifiedValue))
                {
                    property.SetValue(changedEntity, modifiedValue);
                }
            }

            return changedEntity;
        }

    }
}
