import { render, screen } from '@testing-library/react';
import App from './App';

test('renders Activity Tracker header', () => {
  render(<App />);
  const linkElement = screen.getByText(/Activity Tracker/i);
  expect(linkElement).toBeInTheDocument();
});
