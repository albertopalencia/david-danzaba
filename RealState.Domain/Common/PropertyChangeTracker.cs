namespace RealState.Domain.Common
{ public class ChangeTracker<T> where T : class
    {
        public static T ApplyChanges(T existingEntity, T newEntity, out bool hasChanges)
        {
            if (existingEntity == null) throw new ArgumentNullException(nameof(existingEntity));
            if (newEntity == null) throw new ArgumentNullException(nameof(newEntity));


            var updatedEntity = CloneEntity(existingEntity);
            hasChanges = false;

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var existingValue = property.GetValue(existingEntity);
                var newValue = property.GetValue(newEntity);

                if (Equals(existingValue, newValue)) continue;

                property.SetValue(updatedEntity, newValue);
                hasChanges = true;
            }

            return updatedEntity;

        }

        private static T CloneEntity(T entity)
        { 
            return Activator.CreateInstance<T>();
        }
    }
}
