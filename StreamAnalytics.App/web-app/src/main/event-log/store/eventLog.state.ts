import { IAppState } from "global/store";

export const getEventLogs = (state: IAppState) => state.eventLogs.eventLog;
