/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,js}", "./public/*.{html,js}"],
  theme: {
    extend: {
      colors: {
        "theme-color": "var(--theme-color)",
      },
    },
  },
  plugins: [],
};
