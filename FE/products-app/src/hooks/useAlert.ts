import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../store/store';
import { showAlert as showModalAction, hideAlert as hideAlertAction} from '../store/alertSlice';

export function useAlert() {
    const alertState = useSelector((state: RootState) => state.alert);
    const dispatch = useDispatch<AppDispatch>();

    const showAlert = (message: string, type: 'success' | 'error') => {
        dispatch(showModalAction({ message, type }));
    };

    const hideAlert = () => {
        dispatch(hideAlertAction());
    };

  return {
    alertState,
    hideAlert,
    showAlert
  };
}