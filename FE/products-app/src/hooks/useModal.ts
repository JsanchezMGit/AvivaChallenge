import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../store/store';
import { showModal as showModalAction, hideModal as hideModalAction} from '../store/modalSlice';

export function useModal() {
    const modalState = useSelector((state: RootState) => state.modal);
    const dispatch = useDispatch<AppDispatch>();

    const showModal = (modalTitle: string) => {
        dispatch(showModalAction(modalTitle));
    };

    const hideModal = () => {
        dispatch(hideModalAction());
    };

  return {
    modalState,
    showModal,
    hideModal
  };
}