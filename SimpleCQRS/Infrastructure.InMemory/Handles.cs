﻿namespace SimpleCQRS.Infrastructure.InMemory
{
    public interface Handles<T>
    {
        void Handle(T message);
    }
}
