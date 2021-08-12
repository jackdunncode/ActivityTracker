function About() {
  return (
    <div className="p-5 mb-4 bg-light rounded-3">
      <div className="container-fluid py-5">
        <h1 className="display-5 fw-bold">Activity Tracker by Jack Dunn</h1>
        <p className="col-md-8 fs-4">
          This app is designed to make your life easier by allowing you to
          manage when you start and stop activities! Activities will be
          persisted to the backend database which means you can open and close
          this website as much as you like.
        </p>
      </div>
      <div className="card">
        <div className="card-body">
          <h6 className="card-subtitle mb-2 text-muted">Powered by</h6>
          <p className="card-text">React.js, Bootstrap CSS and .NET 5</p>
        </div>
      </div>
    </div>
  );
}

export default About;
