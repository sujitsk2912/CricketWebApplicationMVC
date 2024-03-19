$(document).ready(function () {
    const playerInputs = $('[id^=playerNameInput]');
    const suggestionsContainer = $('#suggestionsContainer');
    const teamIdInput = $('#teamIdInput');
    let submittingForm = false; // Flag to track form submission

    const suggestionsMap = {};

    playerInputs.on('input', function () {
        const query = $(this).val();
        const inputField = $(this); // Reference to the input field


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



    function isPlayerNameUsed(playerName, currentInputId) {
        let isUsed = false;
        playerInputs.each(function () {
            const inputId = $(this).attr('id');
            if (inputId !== currentInputId && $(this).val() === playerName) {
                isUsed = true;
                return false;
            }
        });
        return isUsed;
    }


    function isPlayerNameAdded(playerName) {
        let isAdded = false;
        playerInputs.each(function () {
            if ($(this).val() === playerName) {
                isAdded = true;
                return false;
            }
        });
        return isAdded;
    }
});
