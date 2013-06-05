using System.Web.Mvc;

namespace MongoContacts.Helpers {

    public class ObjectIdBinder : IModelBinder {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            return new MongoDB.Bson.ObjectId(result.AttemptedValue);
        }
    }
}