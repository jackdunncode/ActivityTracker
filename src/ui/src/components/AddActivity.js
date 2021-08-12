import { useState } from 'react';

import { toast } from 'react-toastify';

function AddActivity({ onAdd }) {
  const defaultStartImmediately = true;

  const [name, setName] = useState('');
  const [startImmediately, setStartImmediately] = useState(
    defaultStartImmediately
  );

  const resetForm = () => {
    setName('');
    setStartImmediately(defaultStartImmediately);
  };

  const closeModal = () => {
    document.getElementById('close-button').click();
  };

  const onSubmit = (e) => {
    e.preventDefault();

    if (!name) {
      toast.warn('Please specify a name for the activity.', {
        position: 'top-center',
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: false,
        draggable: false,
        progress: undefined,
      });

      return;
    }

    onAdd({ name, startImmediately });
    closeModal();
    resetForm();
  };

  return (
    <>
      <button
        type="button"
        className="btn btn-primary"
        data-bs-toggle="modal"
        data-bs-target="#addActivityModal"
      >
        <i className="bi-plus-circle"></i>
        Add New Activity
      </button>
      <div
        className="modal fade"
        id="addActivityModal"
        tabIndex="-1"
        aria-labelledby="addActivityModalLabel"
        aria-hidden="true"
      >
        <div className="modal-dialog">
          <form onSubmit={onSubmit}>
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title" id="addActivityModalLabel">
                  Add New Activity
                </h5>
                <button
                  type="button"
                  className="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                />
              </div>
              <div className="modal-body">
                <div className="mb-3">
                  <label htmlFor="name-input" className="form-label">
                    Activity name
                  </label>
                  <input
                    id="name-input"
                    type="text"
                    className="form-control"
                    placeholder="For example: Making dinner"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                  />
                </div>
                <div className="mb-3">
                  <div className="form-check">
                    <label
                      className="form-check-label"
                      htmlFor="start-immediately-input"
                    >
                      Start immediately
                    </label>
                    <input
                      className="form-check-input"
                      type="checkbox"
                      id="start-immediately-input"
                      checked={startImmediately}
                      value={startImmediately}
                      onChange={(e) =>
                        setStartImmediately(e.currentTarget.checked)
                      }
                    />
                  </div>
                </div>
              </div>
              <div className="modal-footer">
                <button
                  type="button"
                  id="close-button"
                  className="btn btn-secondary"
                  data-bs-dismiss="modal"
                  onClick={resetForm}
                >
                  Close
                </button>
                <input
                  type="submit"
                  value="Save changes"
                  className="btn btn-primary"
                />
              </div>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}

export default AddActivity;
