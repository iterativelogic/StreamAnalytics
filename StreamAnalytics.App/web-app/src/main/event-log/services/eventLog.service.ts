import {
  IEventDataModel,
  IEventLogModel,
} from "main/event-log/models/eventLog.model";

class EventLogService {
  private static _instance: EventLogService;

  private constructor() {}

  public static getInstance = () => {
    if (EventLogService._instance != null) return EventLogService._instance;

    EventLogService._instance = new EventLogService();
    return EventLogService._instance;
  };

  public getFakeEventLog = () => {
    const eventTemplate: IEventDataModel = {
      timestamp: "Today",
      tagName: "Tag One",
      assetName: "Asset Public",
      streamType: "Problem",
      state: "True",
      description: "Hello log",
      source: "CEP",
      severity: "High",
      quality: "True",
    };

    const events: IEventDataModel[] = [];

    for (let i = 0; i < 10; i++) {
      events.push(eventTemplate);
    }

    const model: IEventLogModel = {
      totalCount: 10,
      events: events,
    };

    return model;
  };
}

const eventLogService = EventLogService.getInstance();

export default eventLogService;
