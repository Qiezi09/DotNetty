1、Boss线程负责接收处理新连接，HeadContext=>>ServerBootstrapAcceptor=>>TailContext
2、Worker线程负责接收消息

3、Boss线程注册新连接以后到哪一步结束？

启动过程：
1、new 一个MultithreadEventLoopGroup实例作为Boss，其实质是实例化一个SingleThreadEventLoop对象，负责接收处理新连接
2、new 一个MultithreadEventLoopGroup实例作为Woker， 其实质是实例化当前处理器*2个SingleThreadEventLoop对象，不断循环接收新消息
3、new 一个ServerBootstrap对象，配置相关io参数，用来启动dotnetty
4、
