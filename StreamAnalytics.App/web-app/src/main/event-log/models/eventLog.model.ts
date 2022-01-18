export interface IEventDataModel {
  timestamp: String;
  tagName: String;
  assetName: String;
  streamType: String;
  state: String;
  description: String;
  source: String;
  severity: String;
  quality: String;
}

export interface IEventLogModel {
  totalCount: number;
  events: IEventDataModel[];
}

export class EventLogModel implements IEventLogModel {
  public totalCount: number;
  public events: IEventDataModel[];

  public constructor() {
    this.totalCount = 0;
    this.events = [];
  }
}
