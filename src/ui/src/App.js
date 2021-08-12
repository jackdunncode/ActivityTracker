import { BrowserRouter as Router, Route } from 'react-router-dom';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import Header from './components/Header';
import About from './components/About';
import ActivityManager from './components/ActivityManager';

function App() {
  return (
    <Router>
      <div className="container">
        <Header />
        <Route path="/" exact component={ActivityManager} />
        <Route path="/about" component={About} />
      </div>
      <ToastContainer />
    </Router>
  );
}

export default App;
