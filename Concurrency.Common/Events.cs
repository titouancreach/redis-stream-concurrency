using Unitee.EventDriven.Attributes;

namespace Concurrency.Common;

[Subject("CONCURRENCY_START")]
public record StartEvt();

[Subject("CONCURRENCY_EVENT")]
public record Evt(int Count);

[Subject("CONCURRENCY_STOP")]
public record StopEvt();


