$(document).ready(function () {
    const playerInputs = $('[id^=playerNameInput]');
    const suggestionsContainer = $('#suggestionsContainer');
    let submittingForm = false; // Flag to track form submission

    // Initialize a map to store suggestions for each input field
    const suggestionsMap = {};

    playerInputs.on('input', function () {
        const query = $(this).val();
        const inputField = $(this); // Reference to the input field

        // Call the backend API with the query and handle the response
        $.get(`/AddTeam/GetPlayerSuggestions?query=${query}`, function (data) {
            displaySuggestions(data, inputField);
        });
    });

    function displaySuggestions(suggestions, inputField) {
        // Clear previous suggestions
        suggestionsContainer.empty();

        // Display new suggestions in a black box with good design
        const suggestionsBox = $('<div class="suggestions-box"></div>');
        suggestions.forEach(function (suggestion) {
            const suggestionItem = $('<div class="suggestion-item"></div>').text(suggestion);
            suggestionsBox.append(suggestionItem);

            // Attach click event to suggestion items
            suggestionItem.on('click', function () {
                const playerName = $(this).text();
                // Check if the player name is not already present in any other input field
                if (!isPlayerNameUsed(playerName, inputField.attr('id'))) {
                    // Check if the player name is not already added to the same input field
                    if (!isPlayerNameAdded(playerName)) {
                        inputField.val(playerName); // Insert the clicked suggestion into the input box associated with the suggestion
                        suggestionsContainer.empty();
                    } else {
                        // Show alert if player name is already added to the same input field
                        alert("Player name already added to this input field");
                    }
                } else {
                    // Show alert if player name is already used in another field
                    alert("Player name already added to another input field");
                }
            });
        });

        suggestionsContainer.append(suggestionsBox);
    }

    // Function to check if a player name is already used in any other input field
    function isPlayerNameUsed(playerName, currentInputId) {
        let isUsed = false;
        playerInputs.each(function () {
            const inputId = $(this).attr('id');
            if (inputId !== currentInputId && $(this).val() === playerName) {
                isUsed = true;
                return false; // Break the loop if player name is found in any other input field
            }
        });
        return isUsed;
    }

    // Function to check if a player name is already added to the same input field
    function isPlayerNameAdded(playerName) {
        let isAdded = false;
        playerInputs.each(function () {
            if ($(this).val() === playerName) {
                isAdded = true;
                return false; // Break the loop if player name is found in the same input field
            }
        });
        return isAdded;
    }

    // Add clear button functionality
    $(document).on('click', '.clear-input', function () {
        const inputField = $(this).closest('.input-group').find('input');
        inputField.val(''); // Clear the input field
    });

    // Update suggestionsMap on form submission
    // Update suggestionsMap on form submission and check if 11 players are entered
    $('form').on('submit', function (event) {
        submittingForm = true; // Set submittingForm flag to true when form is submitted
        event.preventDefault(); // Prevent the form from submitting initially

        let playersCount = 0;
        playerInputs.each(function () {
            if ($(this).val().trim() !== '') {
                playersCount++;
            }
        });

        if (playersCount < 11) {
            alert("11 players required");
        } else {
            // Allow the form to submit if 11 players are entered
            $(this).off('submit').submit();
        }
    });
});
