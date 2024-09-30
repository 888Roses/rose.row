using System;

namespace rose.row.easy_package.events
{
    public struct DynamicEvent
    {
        public Action before;
        public Action after;
    }

    public struct DynamicEvent<A>
    {
        public Action<A> before;
        public Action<A> after;
    }

    public struct DynamicEvent<A, B>
    {
        public Action<A, B> before;
        public Action<A, B> after;
    }

    public struct DynamicEvent<A, B, C>
    {
        public Action<A, B, C> before;
        public Action<A, B, C> after;
    }

    public struct DynamicEvent<A, B, C, D>
    {
        public Action<A, B, C, D> before;
        public Action<A, B, C, D> after;
    }

    public struct DynamicEvent<A, B, C, D, E>
    {
        public Action<A, B, C, D, E> before;
        public Action<A, B, C, D, E> after;
    }

    public struct DynamicEvent<A, B, C, D, E, F>
    {
        public Action<A, B, C, D, E, F> before;
        public Action<A, B, C, D, E, F> after;
    }

    public struct DynamicEvent<A, B, C, D, E, F, G>
    {
        public Action<A, B, C, D, E, F, G> before;
        public Action<A, B, C, D, E, F, G> after;
    }

    public struct DynamicEvent<A, B, C, D, E, F, G, H>
    {
        public Action<A, B, C, D, E, F, G, H> before;
        public Action<A, B, C, D, E, F, G, H> after;
    }

    public struct DynamicEvent<A, B, C, D, E, F, G, H, I>
    {
        public Action<A, B, C, D, E, F, G, H, I> before;
        public Action<A, B, C, D, E, F, G, H, I> after;
    }
}