import { IStoreAction } from "global/actions";
import * as actions from "./eventLog.actions";
import { IEventLogModel } from "main/event-log/models/eventLog.model";

export interface IEventLogState {
  eventLog: IEventLogModel;
}

const initialState: IEventLogState = {
  eventLog: {
    events: [],
    totalCount: 0,
  },
};

export const eventLogReducer = (
  state: IEventLogState = initialState,
  action: IStoreAction
): IEventLogState => {
  switch (action.type) {
    case actions.SET_EVENT_LOGS:
      return { ...state, eventLog: { ...action.payload } };
    default:
      return { ...state };
  }
};
