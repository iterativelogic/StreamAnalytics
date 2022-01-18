import { IEventLogModel } from "../models/eventLog.model";

export const GET_EVENT_LOGS = "GET_EVENT_LOGS";
export const SET_EVENT_LOGS = "SET_EVENT_LOGS";

export const getEventLogs = () => ({
  type: GET_EVENT_LOGS,
});

export const setEventLogs = (logs: IEventLogModel) => ({
  type: SET_EVENT_LOGS,
  payload: logs,
});
