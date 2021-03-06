﻿namespace BaseLibrary
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }
}
