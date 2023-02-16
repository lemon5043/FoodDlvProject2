/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,js}",
    "./public/*.{html,js}",
    "./node_modules/tw-elements/dist/js/**/*.js",
  ],
  theme: {
    extend: {
      colors: {
        "theme-color": "var(--theme-color)",
      },
    },
  },
  plugins: [require("tw-elements/dist/plugin")],
};
