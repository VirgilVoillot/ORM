using System;

namespace ORM {

    public interface ITransformerSQL{
        
        SQLconstruction createSelectRequest<T>(T entity) where T:Entity;
        SQLconstruction createDeleteRequest<T>(T entity) where T:Entity;
        SQLconstruction createUpdateRequest<T>(T entity) where T:Entity;
    }
}