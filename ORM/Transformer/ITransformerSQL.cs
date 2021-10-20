using System;

namespace ORM {

    public interface ITransformerSQL{
        
        SQLconstruction createSelectRequest<T>(T entity);
        SQLconstruction createDeleteRequest<T>(T entity);

    }
}